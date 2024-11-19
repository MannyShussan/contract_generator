using definitivo.Pdf.TextosJson;
using definitivo.Pdf.TextosJSON;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace definitivo.Pdf;

public class EscritorPdf
{
    private PdfWriter Writer { get; set; }
    private PdfDocument Pdf { get; set; }
    private Document Document { get; set; }
    private Dictionary<string, string> Campos = new Dictionary<string, string>();

    public EscritorPdf(Dictionary<string, string> campos)
    {
        string agora = DateTime.Now.ToString("yyyMMdd_HHmmss");
        Writer = new PdfWriter($"C:\\Users\\Public\\Documents\\{agora}.pdf");
        Pdf = new PdfDocument(Writer);
        Document = new Document(Pdf);
        Campos = campos;
        Pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new RodapeHandler(Document));
    }

    public void EscreveSection(string nomeArquivo)
    {
        string conteudoJson = File.ReadAllText($"{nomeArquivo}");
        List<TextoJson> textos = JsonSerializer.Deserialize<List<TextoJson>>(conteudoJson);
        Paragraph section = new Paragraph();
        foreach (var txt in textos)
        {
            string sub = substituirTexto(txt.texto);
            Text t = new Text(sub);
            if (txt.bold) t.SetBold();
            t.SetFontColor(new DeviceRgb(txt.color[0], txt.color[1], txt.color[2])).SetTextAlignment(TextAlignment.JUSTIFIED_ALL);
            section.Add(t);
        }
        Document.Add(section);
    }

    public void EscreveHeader()
    {
        string conteudoJson = File.ReadAllText("header.json");
        List<TextoJson> textos = JsonSerializer.Deserialize<List<TextoJson>>(conteudoJson);
        Paragraph section = new Paragraph();
        foreach (var txt in textos)
        {
            string sub = substituirTexto(txt.texto);
            Text t = new Text(sub);
            if (txt.bold) t.SetBold();
            t.SetFontColor(new DeviceRgb(txt.color[0], txt.color[1], txt.color[2]));
            section.Add(t);
        }
        section.SetTextAlignment(TextAlignment.JUSTIFIED_ALL)
            .SetMarginLeft(Pdf.GetDefaultPageSize().GetWidth() * (float)0.3)
            .SetMarginTop(30)
            .SetMarginBottom(10);
        Document.Add(section);
    }

    public void EscreveTabelaAssinatura()
    {
        string conteudoJson = File.ReadAllText("signatureTable.json");
        List<TabelaJson> tabelas = JsonSerializer.Deserialize<List<TabelaJson>>(conteudoJson);
        Table tabela = new Table(3).SetWidth(UnitValue.CreatePercentValue(100)).SetMarginTop(15);
        int linha = 0;
        foreach (var tab in tabelas)
        {

            int margemBaixa;
            if (linha++ == 3 || linha == 5 || linha == 7) margemBaixa = 30;
            else margemBaixa = 0;
            Paragraph p1 = new Paragraph(tab.coluna1).SetBold().SetMarginBottom(margemBaixa);
            if (tab.coluna1 == "..") p1.SetFontColor(new DeviceRgb(255, 255, 255));
            Cell col1 = new Cell()
                .Add(p1)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER);

            Paragraph p2 = new Paragraph(tab.coluna2).SetMarginBottom(margemBaixa);
            if (tab.coluna2.StartsWith("[")) p2.SetFontColor(new DeviceRgb(0, 176, 240));
            if (tab.coluna2.StartsWith("__")) p2.SetBackgroundColor(new DeviceRgb(255, 255, 0)).SetBold();
            Cell col2 = new Cell()
                .Add(p2)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER);

            Paragraph p3 = new Paragraph(tab.coluna3).SetMarginBottom(margemBaixa);
            if (tab.coluna3.StartsWith("[")) p3.SetFontColor(new DeviceRgb(0, 176, 240));
            if (tab.coluna3.StartsWith("__")) p3.SetBackgroundColor(new DeviceRgb(255, 255, 0)).SetBold();
            Cell col3 = new Cell()
                .Add(p3)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER);


            tabela.AddCell(col1);
            tabela.AddCell(col2);
            tabela.AddCell(col3);
            tabela.SetMarginBottom(margemBaixa);
        }
        Document.Add(tabela);
    }

    public void CriaDocumento()
    {
        EscreveHeader();
        EscreveSection("body.json");
        Document.Add(new AreaBreak());
        EscreveSection("signature.json");
        EscreveTabelaAssinatura();
        Document.Close();
    }

    private string substituirTexto(string txt)
    {
        return Regex.Replace(txt, @"\[[^\]]+\]", match =>
        {
            string chave = match.Value; // Obtém a chave original do texto (ex.: [nome])
            return Campos.ContainsKey(chave) ? Campos[chave] : chave; // Substitui ou mantém o original
        });
    }
}

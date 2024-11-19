using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Layout;

public class RodapeHandler : IEventHandler
{
    private readonly Document _document;

    public RodapeHandler(Document document)
    {
        _document = document;
    }

    public void HandleEvent(Event @event)
    {
        // Obtém o evento e o número da página
        PdfDocumentEvent pdfEvent = (PdfDocumentEvent)@event;
        PdfPage page = pdfEvent.GetPage();

        // Adiciona o rodapé no canvas da página
        PdfCanvas canvas = new PdfCanvas(page);
        string ano = DateTime.Now.ToString("yyyy");
        canvas.BeginText()
            .SetFontAndSize(_document.GetPdfDocument().GetDefaultFont(), 10) // Fonte padrão
            .MoveText(30, 20) // Posição (x, y) no rodapé (ajuste conforme necessário)
            .ShowText($"V {ano}") // Texto do rodapé
            .EndText()
            .Release();
    }
}
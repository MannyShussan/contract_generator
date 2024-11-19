using definitivo.Pdf;
using System.Text.RegularExpressions;
using iText.Layout.Element;

List<string> list = new List<string>()
{
    "[AnoAssinado]",
    "[NomeConstrutora]",
    "[Adquirentes]",
    "[NumeroApto]",
    "[Torre]",
    "[NomeEmpreendimento]",
    "[OpcaoPlantaEscolhida]",
    "[NumeroOrcamento]",
    "[CustoTotalNumero]",
    "[CustoTotalExtenso]",
    "[MetodoPagamentoEscolhido]",
    "[CustoRescisaoNumero]",
    "[CustoRescisaoExtenso]",
    "[DataCompraImovel]",
    "[DataCustomizacao]",
    "[Cidade]",
    "[DiaAssinado]",
    "[MesAssinadoExtenso]",
    "[NomeConstrutora0]",
    "[NomeConstrutora1]",
    "[NomeAdquirente0]",
    "[NomeAdquirente1]",
    "[NomeTestemunha0]",
    "[NomeTestemunha1]"
};
Dictionary<string, string> campos = new Dictionary<string, string>();
campos.Add("[NomeConstrutora]", "ABC Test");
campos.Add("[Adquirentes]", "Sra. User e Sr. Test");
campos.Add("[NumeroApto]", "1234");
campos.Add("[Torre]", "1");
campos.Add("[NomeEmpreendimento]", "Test Building");
campos.Add("[OpcaoPlantaEscolhida]", "TIPO 1 - 2 dormitórios e 1 suite (50m²)");
campos.Add("[NumeroOrcamento]", "0000-2024-1");
campos.Add("[CustoTotalNumero]", "1500,94");
campos.Add("[CustoTotalExtenso]", "Mil e quinhentos reais e noventa e quatro centavos");
campos.Add("[ValorNumeroParcela1]", "1500,94");
campos.Add("[ValorNumeroParcelaExtenso]", "Mil e quinhentos reais e noventa e quatro centavos");
campos.Add("[VencimentoParcela1]", "21/09/2025");
campos.Add("[NumeroParcelas]", "2");
campos.Add("[NumeroParcelasExtenso]", "Dois");
campos.Add("[ValorParcelas]", "750,47");
campos.Add("[ValorParcelasExtenso]", "Setecentos e cinquenta reais e quarenta e sete centavos");
campos.Add("[PrimeiroVencimentoParcelas]", "21/09/2025");
campos.Add("[CustoRescisaoNumero]", "450,282");
campos.Add("[CustoRescisaoExtenso]", "Quatrocentos e cinquenta reais e vinte e oito centavos");
campos.Add("[DataCompraImovel]", "10/04/2010");
campos.Add("[DataCustomizacao]", "20/04/2010");
campos.Add("[Cidade]", "São Paulo");
campos.Add("[DiaAssinado]", "20");
campos.Add("[MesAssinadoExtenso]", "Abril");
campos.Add("[AnoAssinado]", "2010");
campos.Add("[NomeConstrutora0]", "Admin Test 1");
campos.Add("[NomeConstrutora1]", "Admin Test 2");
campos.Add("[NomeAdquirente0]", "User Test Foo");
campos.Add("[NomeAdquirente1]", "Test Test Bar");
campos.Add("[NomeTestemunha0]", "ABC ");
campos.Add("[NomeTestemunha1]", "CDB");
campos.Add("[EmpresaAdquirente]", "Ficticious SA");


foreach (string key in list)
{
    //string input = string.Empty;
    //do
    //{
    //    Console.WriteLine($"{Regex.Replace(Regex.Replace(key, @"\W", ""), @"([A-Z]{1})([a-z]{1,})", "$1$2 ")}:");
    //    input = Console.ReadLine() ?? string.Empty;
    //} while (input == string.Empty);
    //campos.Add(key, input);
}



Console.ReadKey();
Console.Clear();
var pdf = new EscritorPdf(campos);
pdf.CriaDocumento();
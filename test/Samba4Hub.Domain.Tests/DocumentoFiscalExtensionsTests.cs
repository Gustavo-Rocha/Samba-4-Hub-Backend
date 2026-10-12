using Samba4Hub.Domain.Extensions;
using Xunit;

namespace Samba4Hub.Domain.Tests;

public class DocumentoFiscalExtensionsTests
{
    [Theory]
    [InlineData("52998224725", true)]
    [InlineData("529.982.247-25", true)]
    [InlineData("11111111111", false)] // Repetidos
    [InlineData("12345678900", false)] // DV inválido
    [InlineData("123", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void EhCpfValido_DeveValidarCorretamente(string? cpf, bool esperado)
    {
        var resultado = cpf.EhCpfValido();
        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData("11222333000181", true)]
    [InlineData("11.222.333/0001-81", true)]
    [InlineData("12ABC34501DE35", true)] // Alfanumérico
    [InlineData("12.ABC.345/01DE-35", true)]
    [InlineData("12abc34501de35", true)]
    [InlineData("12ABC34501DE99", false)]
    [InlineData("AAAAAAAAAAAAAA", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void EhCnpjValido_DeveValidarCorretamente(string? cnpj, bool esperado)
    {
        var resultado = cnpj.EhCnpjValido();
        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData("529.982.247-25", true)]
    [InlineData("12.ABC.345/01DE-35", true)]
    [InlineData("00000000000", false)]
    [InlineData("invalid", false)]
    [InlineData(null, false)]
    public void EhDocumentoFiscalValido_DeveValidarCpfOuCnpj(string? documento, bool esperado)
    {
        var resultado = documento.EhDocumentoFiscalValido();
        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(" 12.345.678/0001-90 ", "12345678000190")]
    [InlineData("12.abc.345/01de-35", "12ABC34501DE35")]
    [InlineData(null, "")]
    public void SanitizarDocumentoFiscal_DeveRemoverMascaraENormalizar(string? entrada, string esperado)
    {
        var resultado = entrada.SanitizarDocumentoFiscal();
        Assert.Equal(esperado, resultado);
    }
}

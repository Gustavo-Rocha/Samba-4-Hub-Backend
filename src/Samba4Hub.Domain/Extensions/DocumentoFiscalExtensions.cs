using System.Text.RegularExpressions;

namespace Samba4Hub.Domain.Extensions;

/// <summary>
/// Métodos de extensão para higienização e validação fiscal de CPF e CNPJ (incluindo o padrão alfanumérico IN RFB 2.229/2024).
/// </summary>
public static class DocumentoFiscalExtensions
{
    private static readonly Regex CaracteresInvalidosRegex = new(@"[^0-9A-Za-z]", RegexOptions.Compiled);
    private static readonly Regex CpfRegex = new(@"^[0-9]{11}$", RegexOptions.Compiled);
    private static readonly Regex CnpjRegex = new(@"^[0-9A-Z]{12}[0-9]{2}$", RegexOptions.Compiled);

    private static readonly int[] PesosCpfPrimeiroDigito = [10, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] PesosCpfSegundoDigito = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

    private static readonly int[] PesosCnpjPrimeiroDigito = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] PesosCnpjSegundoDigito = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    /// <summary>
    /// Remove pontuações, barras, traços e espaços, padronizando a cadeia em caracteres alfanuméricos em caixa alta.
    /// </summary>
    /// <param name="documento">Texto bruto do documento fiscal com ou sem máscara.</param>
    /// <returns>String higienizada ou vazia se nulo.</returns>
    public static string SanitizarDocumentoFiscal(this string? documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
        {
            return string.Empty;
        }

        return CaracteresInvalidosRegex.Replace(documento, string.Empty).ToUpperInvariant();
    }

    /// <summary>
    /// Verifica se a cadeia representa um CPF autêntico, calculando os dígitos verificadores em módulo 11.
    /// Aceita strings com ou sem máscara.
    /// </summary>
    /// <param name="cpf">Número do CPF a ser validado.</param>
    /// <returns><c>true</c> se o CPF for válido; caso contrário, <c>false</c>.</returns>
    public static bool EhCpfValido(this string? cpf)
    {
        var higienizado = cpf.SanitizarDocumentoFiscal();

        if (higienizado.Length != 11 || !CpfRegex.IsMatch(higienizado))
        {
            return false;
        }

        // Rejeita sequências de dígitos repetidos conhecidas (ex: 111.111.111-11)
        if (higienizado.Distinct().Count() == 1)
        {
            return false;
        }

        var soma1 = 0;
        for (var i = 0; i < 9; i++)
        {
            soma1 += (higienizado[i] - '0') * PesosCpfPrimeiroDigito[i];
        }

        var resto1 = soma1 % 11;
        var digito1 = resto1 < 2 ? 0 : 11 - resto1;

        if (higienizado[9] - '0' != digito1)
        {
            return false;
        }

        var soma2 = 0;
        for (var i = 0; i < 10; i++)
        {
            soma2 += (higienizado[i] - '0') * PesosCpfSegundoDigito[i];
        }

        var resto2 = soma2 % 11;
        var digito2 = resto2 < 2 ? 0 : 11 - resto2;

        return higienizado[10] - '0' == digito2;
    }

    /// <summary>
    /// Verifica se a cadeia representa um CNPJ autêntico (tradicional numérico ou alfanumérico conforme IN RFB 2.229/2024).
    /// Aceita strings com ou sem máscara.
    /// </summary>
    /// <param name="cnpj">Número do CNPJ a ser validado.</param>
    /// <returns><c>true</c> se o CNPJ for válido; caso contrário, <c>false</c>.</returns>
    public static bool EhCnpjValido(this string? cnpj)
    {
        var higienizado = cnpj.SanitizarDocumentoFiscal();

        if (higienizado.Length != 14 || !CnpjRegex.IsMatch(higienizado))
        {
            return false;
        }

        // Rejeita sequências de caracteres idênticos
        if (higienizado.Distinct().Count() == 1)
        {
            return false;
        }

        // Cálculo do primeiro dígito verificador (posições 0 a 11)
        var soma1 = 0;
        for (var i = 0; i < 12; i++)
        {
            var valorCaractere = ObterValorCaractereCnpj(higienizado[i]);
            soma1 += valorCaractere * PesosCnpjPrimeiroDigito[i];
        }

        var resto1 = soma1 % 11;
        var digito1 = resto1 < 2 ? 0 : 11 - resto1;

        if (higienizado[12] - '0' != digito1)
        {
            return false;
        }

        // Cálculo do segundo dígito verificador (posições 0 a 12)
        var soma2 = 0;
        for (var i = 0; i < 12; i++)
        {
            var valorCaractere = ObterValorCaractereCnpj(higienizado[i]);
            soma2 += valorCaractere * PesosCnpjSegundoDigito[i];
        }
        soma2 += digito1 * PesosCnpjSegundoDigito[12];

        var resto2 = soma2 % 11;
        var digito2 = resto2 < 2 ? 0 : 11 - resto2;

        return higienizado[13] - '0' == digito2;
    }

    /// <summary>
    /// Valida se a cadeia representa um documento fiscal autêntico (CPF com 11 dígitos ou CNPJ com 14 posições).
    /// </summary>
    /// <param name="documento">Texto do documento fiscal com ou sem máscara.</param>
    /// <returns><c>true</c> se for um CPF ou CNPJ autêntico; caso contrário, <c>false</c>.</returns>
    public static bool EhDocumentoFiscalValido(this string? documento)
    {
        var higienizado = documento.SanitizarDocumentoFiscal();

        return higienizado.Length switch
        {
            11 => higienizado.EhCpfValido(),
            14 => higienizado.EhCnpjValido(),
            _ => false
        };
    }

    /// <summary>
    /// Identifica se o documento higienizado possui a extensão de um CPF (11 dígitos).
    /// </summary>
    /// <param name="documento">Texto do documento.</param>
    /// <returns><c>true</c> se possuir 11 dígitos após sanitização.</returns>
    public static bool EhCpf(this string? documento)
    {
        return documento.SanitizarDocumentoFiscal().Length == 11;
    }

    /// <summary>
    /// Identifica se o documento higienizado possui a extensão de um CNPJ (14 posições).
    /// </summary>
    /// <param name="documento">Texto do documento.</param>
    /// <returns><c>true</c> se possuir 14 posições após sanitização.</returns>
    public static bool EhCnpj(this string? documento)
    {
        return documento.SanitizarDocumentoFiscal().Length == 14;
    }

    private static int ObterValorCaractereCnpj(char c)
    {
        // Norma IN RFB 2.229/2024: Código ASCII - 48
        // '0'-'9' (ASCII 48-57) -> 0 a 9
        // 'A'-'Z' (ASCII 65-90) -> 17 a 42
        return c - 48;
    }
}

using Newtonsoft.Json;

namespace TaikoSwitchConverter.DTO.Switch;

public class WordDataSingle
{
    [JsonProperty("key", Required = Required.Always)]
    public string Key { get; set; }
    
    [JsonProperty("japaneseText", Required = Required.Always)]
    public string JapaneseText { get; set; }
    
    [JsonProperty("englishUsText", Required = Required.Always)]
    public string EnglishText { get; set; }
    [JsonProperty("englishUsFontType")]
    public int EnglishFontType { get; set; }
    
    [JsonProperty("frenchText", Required = Required.Always)]
    public string FrenchText { get; set; }
    [JsonProperty("frenchFontType")]
    public int FrenchFontType { get; set; }
    
    [JsonProperty("italianText", Required = Required.Always)]
    public string ItalianText { get; set; }
    [JsonProperty("italianFontType")]
    public int ItalianFontType { get; set; }
    
    [JsonProperty("germanText", Required = Required.Always)]
    public string GermanText { get; set; }
    [JsonProperty("germanFontType")]
    public int GermanFontType { get; set; }
    
    [JsonProperty("spanishText", Required = Required.Always)]
    public string SpanishText { get; set; }
    [JsonProperty("spanishFontType")]
    public int SpanishFontType { get; set; }
    
    [JsonProperty("chineseTText", Required = Required.Always)]
    public string TaiwaneseText { get; set; }
    [JsonProperty("chineseTFontType")]
    public int TaiwaneseFontType { get; set; }
    
    [JsonProperty("chineseSText", Required = Required.Always)]
    public string ChineseSimplifiedText { get; set; }
    [JsonProperty("chineseSFontType")]
    public int ChineseSimplifiedFontType { get; set; }
    
    [JsonProperty("koreanText", Required = Required.Always)]
    public string KoreanText { get; set; }
    [JsonProperty("koreanFontType")]
    public int KoreanFontType { get; set; }
}
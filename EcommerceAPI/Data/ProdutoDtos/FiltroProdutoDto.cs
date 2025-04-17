namespace EcommerceAPI.Data.ProdutoDtos
{
    public class FiltroProdutoDto
    {
        public string NomeProd { get; set; }
        public string DescricaoProd { get; set; }
        public double? PesoProd { get; set; }
        public double? AlturaProd { get; set; }
        public double? LarguraProd { get; set; }
        public double? ComprimentoProd { get; set; }
        public double? ValorProd { get; set; }
        public int? QuantidadeProd { get; set; }
        public string CentroDistribuicaoProd { get; set; }
        public bool? StatusProd { get; set; }
        public string OrdemProd { get; set; }
    }
}

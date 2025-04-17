namespace EcommerceAPI.Data.CDDtos
{
    public class FiltroCDDto
    {
        public string NomeCD { get; set; }
        public string LogradouroCD { get; set; }
        public int? NumeroCD { get; set; }
        public string ComplementoCD { get; set; }
        public string BairroCD { get; set; }
        public string CEP { get; set; }
        public string LocalidadeCD { get; set; }
        public string UF { get; set; }
        public bool? StatusCD { get; set; }


        public int ItensPagina { get; set; } = 10;
        public int Pagina { get; set; } = 0;
        public string Ordem { get; set; }
    }
}

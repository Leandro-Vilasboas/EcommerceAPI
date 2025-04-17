namespace EcommerceAPI.Data
{
    public class PaginacaoDto
    {
        public int ItensPagina { get; set; } = 10;
        public int Pagina { get; set; } = 0;
        public string Ordem { get; set; }
    }
}
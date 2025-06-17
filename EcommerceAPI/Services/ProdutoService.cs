using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.ProdutoDtos;
using EcommerceAPI.Models;
using System;
using System.Linq;

namespace EcommerceAPI.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProdutoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadProdutoDto AdicionarProduto(CreateProdutoDto produtoDto)
        {
            var nomeExistente = _context.Produtos.FirstOrDefault(c => c.Nome == produtoDto.Nome);
            CategoriaModel categoria = _context.Categorias.FirstOrDefault(c => c.Id == produtoDto.CategoriaId);
            SubcategoriaModel subcategoria = _context.Subcategorias.FirstOrDefault(c => c.Id == produtoDto.SubcategoriaId);
            ProdutoModel produto = _mapper.Map<ProdutoModel>(produtoDto);

            if (categoria == null || subcategoria == null)
            {
                return null;
            }

            if (categoria.Status == false || subcategoria.Status == false)
            {
                return null;
            }

            if (produtoDto.Nome == categoria.Nome || produtoDto.Nome == subcategoria.Nome)
            {
                return null;
            }

            if (nomeExistente == null)
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                //retorna o caminho em que foi criado o produto
                return _mapper.Map<ReadProdutoDto>(produto);
            }
            return null;
        }

        public UpdateProdutoDto ModificarProduto(int id, UpdateProdutoDto produtoDto)
        {
            var nomeExistente = _context.Produtos.FirstOrDefault(c => c.Nome == produtoDto.Nome);
            ProdutoModel produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);

            if (produto == null)
            {
                return null;
            }
            if (nomeExistente != null)
            {
                return null;
            }
            _mapper.Map(produtoDto, produto);
            produto.DataDeAlteracao = DateTime.Now;
            _context.SaveChanges();
            return null;
        }

        public UpdateProdutoDto ModificarStatus(int id)
        {
            ProdutoModel produto = _context.Produtos.FirstOrDefault(c => c.Id == id);
            if (produto == null)
            {
                return null;
            }
            if (produto.Status == true)
            {
                produto.Status = false;
                produto.DataDeAlteracao = DateTime.Now;
            }
            else
            {
                produto.Status = true; ;
                produto.DataDeAlteracao = DateTime.Now;
            }
            _context.SaveChanges();
            return null;
        }

        public ReadProdutoDto DeletarProduto(int id)
        {
            ProdutoModel produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            _context.Remove(produto);
            _context.SaveChanges();
            return null;
        }
    }
}

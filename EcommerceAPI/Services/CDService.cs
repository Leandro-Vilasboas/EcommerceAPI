
using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.CDDtos;
using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommerceAPI.Services
{
    public class CDService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly CDRepository _repository;

        public CDService(AppDbContext context, IMapper mapper, CDRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CDModel> BuscarViaCep(string cep)
        {
            HttpClient httpClient = new();
            var consulta = await httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var retorno = await consulta.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CDModel>(retorno);
            return result;
        }

        public async Task<CDModel> CadastrarCD(CreateCDDto cdDto)
        {
            var buscarCEP = await BuscarViaCep(cdDto.CEP);
            var nomeExiste = _context.CentroDeDistribuicao.FirstOrDefault(c => c.Nome == cdDto.Nome);

            if (nomeExiste == null)
            {
                cdDto.Logradouro = buscarCEP.Logradouro;
                cdDto.Localidade = buscarCEP.Localidade;
                cdDto.Bairro = buscarCEP.Bairro;
                cdDto.UF = buscarCEP.UF;
                CDModel cd = _mapper.Map<CDModel>(cdDto);
                _context.CentroDeDistribuicao.Add(cd);
                _context.SaveChanges();
                return _mapper.Map<CDModel>(cd);
            }
            return null;
        }

        public async Task<CDModel> EditarCD(int id, UpdateCDDto cdDto)
        {
            CDModel cd = _context.CentroDeDistribuicao.FirstOrDefault(c => c.Id == id);
            var nomeExiste = _context.CentroDeDistribuicao.FirstOrDefault(c => c.Nome == cdDto.Nome);

            if (cd == null)
            {
                return null;
            }
            if (nomeExiste != null)
            {
                return null;
            }
            var buscarCEP = await BuscarViaCep(cdDto.CEP);

            cdDto.Logradouro = buscarCEP.Logradouro;
            cdDto.Localidade = buscarCEP.Localidade;
            cdDto.Bairro = buscarCEP.Bairro;
            cdDto.UF = buscarCEP.UF;
            _mapper.Map(cdDto, cd);
            cd.DataDeAlteracao = DateTime.Now;
            _context.SaveChanges();
            return cd;
        }

        public UpdateCDDto ModificarStatus(int id)
        {
            CDModel cdModel = _context.CentroDeDistribuicao.FirstOrDefault(c => c.Id == id);
            if (cdModel == null)
            {
                return null;
            }
            if (cdModel.Status == true)
            {
                cdModel.Status = false;
                cdModel.DataDeAlteracao = DateTime.Now;
            }
            else
            {
                cdModel.Status = true; ;
                cdModel.DataDeAlteracao = DateTime.Now;
            }
            _context.SaveChanges();
            return null;
        }

        public ReadCDDto DeletarCD(int id)
        {
            CDModel cd = _context.CentroDeDistribuicao.FirstOrDefault(c => c.Id == id);
            _context.Remove(cd);
            _context.SaveChanges();
            return null;
        }
    }
}

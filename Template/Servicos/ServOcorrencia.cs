using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using Ocorrencias.DTO;

namespace Ocorrencias.Servicos
{
    public class ServOcorrencia
    {
        private readonly DataContext _context;

        public ServOcorrencia(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Ocorrencia>> Listar()
        {
            return await _context.Ocorrencias.ToListAsync();
        }

        public async Task<Ocorrencia?> BuscarPorId(int id)
        {
            return await _context.Ocorrencias.FindAsync(id);
        }

        public async Task<Ocorrencia> Criar(Ocorrencia ocorrencia)
        {
            using HttpClient client = new();

            var request = new RoteamentoRequestDTO
            {
                LocalIncendio = new LocalizacaoDTO
                {
                    Latitude = ocorrencia.Latitude,
                    Longitude = ocorrencia.Longitude
                }
            };

            var response = await client.PostAsJsonAsync(
                "http://localhost:5089/api/Mapa/rota-mais-proxima",
                request
            );

            var respostaBruta = await response.Content.ReadAsStringAsync();

            Console.WriteLine("RESPOSTA DO MAPA:");
            Console.WriteLine(respostaBruta);

            var conteudo = await response.Content.ReadFromJsonAsync<RoteamentoResponseDTO>();
            
            if (conteudo == null)
            {
                Console.WriteLine("Resposta do Mapa é nula.");
                throw new Exception("Não foi possível obter a rota mais próxima.");
            }

            ocorrencia.Distancia = conteudo.DistanciaEstimada;
            ocorrencia.TempoEstimaodo = conteudo.DuracaoEstimada;

            _context.Ocorrencias.Add(ocorrencia);
            await _context.SaveChangesAsync();

            return ocorrencia;
        }

        public async Task<bool> Atualizar(int id, Ocorrencia ocorrencia)
        {
            var existente = await _context.Ocorrencias.FindAsync(id);

            if (existente == null)
                return false;

            existente.Tipo = ocorrencia.Tipo;
            existente.Descricao = ocorrencia.Descricao;
            existente.Latitude = ocorrencia.Latitude;
            existente.Longitude = ocorrencia.Longitude;
            existente.Status = ocorrencia.Status;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Excluir(int id)
        {
            var ocorrencia = await _context.Ocorrencias.FindAsync(id);

            if (ocorrencia == null)
                return false;

            _context.Ocorrencias.Remove(ocorrencia);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
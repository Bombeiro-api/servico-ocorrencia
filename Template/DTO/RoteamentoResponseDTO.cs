namespace Ocorrencias.DTO;

public class RoteamentoResponseDTO
{
    public CorporacaoBombeiroDTO CorporacaoMaisProxima { get; set; } = new();

    public int ViaturaId { get; set; }

    public string DuracaoEstimada { get; set; } = string.Empty;

    public string DistanciaEstimada { get; set; } = string.Empty;

    public List<PassoRotaDTO> Passos { get; set; } = new();

    public string PolylineEncoded { get; set; } = string.Empty;
}
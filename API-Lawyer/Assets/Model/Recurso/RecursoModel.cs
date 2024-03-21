namespace API_Lawyer.Assets.Model.Recurso
{
    public class RecursoModel
    {
        private string Id { get; set; }
        private enum Tipo { Incidentes, Acidentais, Recursos, Sentenças }
        private string Descricao { get; set; }
        private DateTime Data { get; set; }
        private string IdNumeroProceso { get; set; }




    }
}

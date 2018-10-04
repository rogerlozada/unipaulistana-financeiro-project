namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public class DashboardService : IDashboardService
    {
        public DashboardService(IDashboardRepository repository)
        {
            this.repository = repository;
        }

        readonly IDashboardRepository repository;

        public Dashboard DashboardUsuario(int usuarioID)
            => this.repository.DashboardUsuario(usuarioID);
    }
}

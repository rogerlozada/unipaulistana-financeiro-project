namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public interface IDashboardRepository
    {
        Dashboard DashboardUsuario(int usuarioID);
    } 
}

﻿using TicketApplication.Data.Entities;

namespace TicketApplication.Data.EFRepositories
{
    public interface IBonRepositoryEF
    {
        Task<Bon?> GetBonById(int id);
        Task<IEnumerable<Bon>> GetAllBonByGhiseuId(int ghiseuId);
        Task<IEnumerable<Bon>> GetAllBon();
        Task<Bon> AddBon(Bon bon);
        Task MarkAsInProgress(int id);
        Task MarkAsReceived(int id);
        Task MarkAsClose(int id);
    }
}

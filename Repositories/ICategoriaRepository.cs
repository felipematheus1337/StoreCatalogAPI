using StoreCatalogAPI.Models;

namespace StoreCatalogAPI.Repositories;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> GetCategorias();
    Categoria GetCaregoria(int id);

    Categoria Create(Categoria categoria);
    Categoria Update (Categoria categoria);

    Categoria Delete(int id);


}

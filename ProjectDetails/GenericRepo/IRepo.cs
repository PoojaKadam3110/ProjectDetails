namespace ProjectDetailsAPI.GenericRepo
{
    public interface IRepo<T> where T : class
    {
        List<T> GetAll();

        T GetById(int Id);
    }
}

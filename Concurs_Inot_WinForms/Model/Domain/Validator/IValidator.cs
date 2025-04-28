namespace Model.Domain.Validator
{
    public interface IValidator<E>
    {
        void Validate(E entity);
    }
}



using Golestan.Core.Extensions;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        public List<Book> Books { get; private set; } = [];

        public Category(string name)
        {
            ValidateName(name);
          
            Name = name;
        }


        public void ChangeName(string name)
        {
            ValidateName(name);
            Name = name;
        }

        private void ValidateName(string name)
        {
            if (!name.IsValidText())
                throw new ValidationException("Category name cannot be empty.");
            if (name.Length > 50)
                throw new ValidationException("Category name cannot exceed 50 characters.");
        }



    }

}

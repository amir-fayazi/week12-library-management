


using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Extensions;

namespace LibraryManagement.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public Category Category { get; private set; }
        public int CategoryId { get; private set; }
        public List<BookLoan> BookLoans { get; private set; }

        public Book()
        {
            
        }
        public Book(string title, Category category)
        {
            ValidateTitle(title);
            ValidateCategory(category);

            Category = category;
            Title = title;
        }


        public void ChangeTitle(string title)
        {
            ValidateTitle(title);
            Title = title;
        }

        public void ChangeCategory(Category category)
        {
            ValidateCategory(category);
            Category = category;
        }

        private void ValidateTitle(string title)
        {
            if (!title.IsValidText())
                throw new ValidationException("Book title cannot be empty."); 
            if (title.Length > 150)
                throw new ValidationException("Book title cannot exceed 150 characters."); 
        }

        private void ValidateCategory(Category category)
        {
            if (category is null)
                throw new ValidationException("Book must have a valid category.");
        }
    }
}

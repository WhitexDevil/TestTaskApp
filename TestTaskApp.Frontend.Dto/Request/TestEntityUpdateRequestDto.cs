using System.ComponentModel.DataAnnotations;

namespace TestTaskApp.Frontend.Dto.Request
{
    public class TestEntityUpdateRequestDto : BaseTestEntityDto
    {
        [Required]
        public int Id { get; set; }
    }

    public class TestEntityAddRequestDto : BaseTestEntityDto
    {
    }

    public class TestEntityDeleteRequestDto 
    {
        [Required]
        public int Id { get; set; }
    }

}

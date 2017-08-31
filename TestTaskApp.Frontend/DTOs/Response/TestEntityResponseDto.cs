using System;

namespace TestTaskApp.Frontend.DTOs.Response
{
    public class TestEntityResponseDto:BaseTestEntityDto
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskModel;

public partial class ToDoItem
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Title { get; set; } = null!;

    [StringLength(50)]
    public string Status { get; set; } = null!;

    public int DaysLeft { get; set; }

    public int CategoryId { get; set; }

   
    public virtual Category Category { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskModel;

public partial class Category
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public virtual ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
}

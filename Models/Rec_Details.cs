using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rec_Tracker.Models;

namespace Rec_Tracker.Models;

public class Rec_Details
{   [Key]
    [Display(Name = "File Number")]
    [Range(1, int.MaxValue, ErrorMessage = "File Number must be 1 or greater.")]
    public int File_Number { get; set; }

    [Required]
    [Display(Name = "File Name")]
    public string File_Name { get; set; }

    [Required]
    [Display(Name = "Organization")]
    public string Organisation { get; set; }

    [Required]
    [Display(Name = "File Type")]
    public string File_Type { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string File_Description { get; set; }

    [Required]
    [Display(Name = "Shelf Number")]
    public string Shelf_Number { get; set; }

    [Required]
    [Display(Name = "Row Number")]
    public string Row_number { get; set; }

    [Display(Name = "Position in Shelf")]
    [Range(1, int.MaxValue, ErrorMessage = "Position must be 1 or greater.")]
    public int Position { get; set; }

}
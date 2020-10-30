﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OneGate.Shared.Models.Order;

namespace OneGate.Backend.Database.Models
{
    [Table("order")]
    public abstract class OrderBase
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        
        [Column("type")]
        [Required]
        public string Type { get; set; }
        
        [Column("asset_id")]
        [Required]
        public int AssetId { get; set; }
        
        [Column("owner_id")]
        [Required]
        public int OwnerId { get; set; }
        
        [Column("state")]
        [Required]
        public string State { get; set; }
        
        [Column("side")]
        [Required]
        public string Side { get; set; }
        
        [Column("quantity")]
        [Required]
        public float Quantity { get; set; }
        
        [ForeignKey(nameof(AssetId))]
        [Required]
        public AssetBase Asset { get; set; }

        [ForeignKey(nameof(OwnerId))]
        [Required]
        public Account Account { get; set; }
    }
}
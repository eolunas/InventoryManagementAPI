import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../inventory.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products: any[] = [];
  errorMessage: string = '';

  constructor(private inventoryService: InventoryService) {}

  ngOnInit(): void {
    this.loadInventory();
  }

  loadInventory(): void {
    this.inventoryService.getInventory().subscribe({
      next: (data) => {
        console.log('API Response:', data); // Depuración
        this.products = data;
      },
      error: (error) => {
        console.error('API Error:', error);
        this.errorMessage = 'Failed to load inventory';
      }
    });
  }
}

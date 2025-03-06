import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../inventory.service';

@Component({
  selector: 'app-product-movement',
  templateUrl: './product-movement.component.html',
  styleUrls: ['./product-movement.component.scss']
})
export class ProductMovementComponent implements OnInit {
  products: any[] = [];
  selectedProductId: number | null = null;
  quantity: number = 0;
  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(private inventoryService: InventoryService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.inventoryService.getInventory().subscribe({
      next: (data) => {
        this.products = data;
      },
      error: () => {
        this.errorMessage = 'Failed to load products';
      }
    });
  }

  onSubmit(): void {
    if (!this.selectedProductId || this.quantity === 0) {
      this.errorMessage = 'Please select a product and enter a valid quantity';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.inventoryService.registerMovement(this.selectedProductId, this.quantity).subscribe({
      next: () => {
        alert('Movement registered successfully');
        this.isLoading = false;
        this.quantity = 0;
      },
      error: () => {
        this.errorMessage = 'Failed to register movement';
        this.isLoading = false;
      }
    });
  }
}

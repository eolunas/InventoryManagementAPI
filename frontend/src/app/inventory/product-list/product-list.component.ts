import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../inventory.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products: any[] = [];
  isModalOpen = false;
  isEditing = false;
  selectedProduct = { id: 0, name: '', quantity: 0 };
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

  openModal(edit: boolean, product: any = { id: 0, name: '', price: 0, stock: 0 }) {
    this.isEditing = edit;
    this.selectedProduct = { ...product };
    this.isModalOpen = true;
  }

  deleteProduct(id: number) {
    this.inventoryService.deleteProduct(id).subscribe({
      next: () => this.loadInventory(),
      error: (err: HttpErrorResponse) => {
        let errorMessage = 'An error occurred while deleting.';
        if (err.status === 404) {
          errorMessage = 'Product not found.';
        } else if (err.status === 422) {
          errorMessage = 'Cannot delete product because it has associated movements.';
        }
        alert(errorMessage);
      }
    });
  }

}

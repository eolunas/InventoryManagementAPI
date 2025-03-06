import { Component, Input, Output, EventEmitter } from '@angular/core';
import { InventoryService } from '../inventory.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent {
  @Input() isOpen = false;
  @Input() isEdit = false;
  @Input() product = { id: 0, name: '', quantity: 0 };
  @Output() close = new EventEmitter<void>();
  @Output() refreshList = new EventEmitter<void>();

  message: string = '';
  messageType: 'success' | 'error' = 'success';

  constructor(private inventoryService: InventoryService) {}

  saveProduct() {
    if (this.isEdit) {
      this.inventoryService.updateProduct(this.product).subscribe({
        next: () => {
          this.showMessage('Product updated successfully!', 'success');
          this.refreshList.emit();
          setTimeout(() => this.closeModal(), 2000);
        },
        error: (err: HttpErrorResponse) => {
          if (err.status === 404) {
            this.showMessage('Product not found.', 'error');
          } else if (err.status === 422) {
            this.showMessage('Quantity must be updated through inventory movements.', 'error');
          }
        }
      });
    } else {
      this.inventoryService.createProduct(this.product).subscribe({
        next: () => {
          this.showMessage('Product created successfully!', 'success');
          this.refreshList.emit();
          setTimeout(() => this.closeModal(), 2000);
        },
        error: (err: HttpErrorResponse) => {
          this.showMessage(this.getErrorMessage(err), 'error');
        }
      });
    }
  }

  getErrorMessage(error: HttpErrorResponse): string {
    if (error.status === 400) {
      return "Invalid data. Please check the fields.";
    } else if (error.status === 404) {
      return "Product not found.";
    } else if (error.status === 409) {
      return "A product with this name already exists.";
    } else {
      return "An unexpected error occurred.";
    }
  }

  showMessage(msg: string, type: 'success' | 'error') {
    this.message = msg;
    this.messageType = type;
  }

  closeModal() {
    this.isOpen = false;
    this.message = ''; // Reset message on close
    this.close.emit();
  }
}

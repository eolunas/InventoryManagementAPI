import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InventoryRoutingModule } from './inventory-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductMovementComponent } from './product-movement/product-movement.component';
import { ProductHistoryComponent } from './product-history/product-history.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { LayoutComponent } from '../core/layout/layout.component';

@NgModule({
  declarations: [
    ProductListComponent,
    ProductMovementComponent,
    ProductHistoryComponent,
    ProductFormComponent,
    LayoutComponent,
  ],
  imports: [CommonModule, FormsModule, InventoryRoutingModule],
})
export class InventoryModule {}

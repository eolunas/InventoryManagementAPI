import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from '../core/layout/layout.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductMovementComponent } from './product-movement/product-movement.component';
import { ProductHistoryComponent } from './product-history/product-history.component';

import { inventoryGuard } from './inventory.guard'; // Guard de seguridad

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [inventoryGuard],
    children: [
      { path: '', component: ProductListComponent },
      { path: 'movement', component: ProductMovementComponent },
      { path: 'history', component: ProductHistoryComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InventoryRoutingModule { }

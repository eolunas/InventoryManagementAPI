import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductMovementComponent } from './product-movement/product-movement.component';
import { ProductHistoryComponent } from './product-history/product-history.component';

import { inventoryGuard } from './inventory.guard'; // Importar el guard actualizado

const routes: Routes = [
  { path: '', component: ProductListComponent, canActivate: [inventoryGuard] },
  { path: 'movement', component: ProductMovementComponent, canActivate: [inventoryGuard] },
  { path: 'history', component: ProductHistoryComponent, canActivate: [inventoryGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InventoryRoutingModule { }

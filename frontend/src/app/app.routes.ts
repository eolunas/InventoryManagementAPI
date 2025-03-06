import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/inventory', pathMatch: 'full' },
  { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) },
  { path: 'inventory', loadChildren: () => import('./inventory/inventory.module').then(m => m.InventoryModule) },
  { path: '**', redirectTo: 'inventory' }
];

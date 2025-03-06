import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {}

  getInventory(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/products/inventory`);
  }

  createProduct(product: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/products`, product);
  }

  updateProduct(product: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/products/${product.id}`, product);
  }

  deleteProduct(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/products/${productId}`);
  }

  registerMovement(productId: number, quantity: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/products/movement`, { productId, quantity });
  }

  getMovements(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/movements`);
  }
}

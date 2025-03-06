import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  private apiUrl = `${environment.apiUrl}/products/inventory`;

  constructor(private http: HttpClient) {}

  getInventory(): Observable<any[]> {
    const token = localStorage.getItem('token'); // Obtener el token del almacenamiento local
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`); // Agregar prefijo 'Bearer'
    return this.http.get<any[]>(this.apiUrl, { headers });
  }
}

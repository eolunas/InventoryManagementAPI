import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent {
  constructor(private router: Router) {}

  logout() {
    localStorage.removeItem('token'); // Eliminar el token de autenticación
    this.router.navigate(['/auth']); // Redirigir al login
  }
}

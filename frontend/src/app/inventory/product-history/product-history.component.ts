import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../inventory.service';

@Component({
  selector: 'app-product-history',
  templateUrl: './product-history.component.html',
  styleUrls: ['./product-history.component.scss']
})
export class ProductHistoryComponent implements OnInit {
  movements: any[] = [];
  errorMessage: string = '';

  constructor(private inventoryService: InventoryService) {}

  ngOnInit(): void {
    this.loadMovements();
  }

  loadMovements(): void {
    this.inventoryService.getMovements().subscribe({
      next: (data) => {
        this.movements = data;
      },
      error: () => {
        this.errorMessage = 'Failed to load movement history';
      }
    });
  }
}

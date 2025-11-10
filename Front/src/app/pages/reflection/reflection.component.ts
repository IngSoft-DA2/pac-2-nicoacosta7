import { Component } from '@angular/core';
import { ReflectionService } from '../../services/reflection.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reflection',
  imports: [CommonModule],
  templateUrl: './reflection.component.html',
  styleUrl: './reflection.component.css'
})
export class ReflectionComponent {
importers: string[] = [];
  loading = false;
  error = '';

  constructor(private reflectionService: ReflectionService) { }

  loadImporters(): void {
    this.loading = true;
    this.error = '';
    this.importers = [];

    this.reflectionService.getAllImporters().subscribe({
      next: (data) => {
        this.importers = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar los importadores';
        this.loading = false;
        console.error('Error:', err);
      }
    });
  }
}

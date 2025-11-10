import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ReflectionService {

  constructor(private http: HttpClient) { }

  getAllImporters() {
    return this.http
      .get<string[]>(`http://localhost:5248/api/Reflection/importers`);
  }
}

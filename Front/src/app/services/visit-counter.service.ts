import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class VisitCounterService {
  private count = 0;

  incrementAndGet(): number {
    this.count += 1;
    return this.count;
  }

  getCount(): number {
    return this.count;
  }
}
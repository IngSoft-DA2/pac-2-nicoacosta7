import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { VisitCounterService } from './services/visit-counter.service';

export const reflectionLimitGuard: CanActivateFn = () => {
  const counter = inject(VisitCounterService);

  const nextValue = counter.incrementAndGet();

  return nextValue > 20 ? false : true;
};

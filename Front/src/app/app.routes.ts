import { Routes } from '@angular/router';
import { ReflectionComponent } from './pages/reflection/reflection.component';
import { ConsignaComponent } from './shared/components/consigna/consigna.component';
import { reflectionLimitGuard } from './reflection-limit.guard';

export const routes: Routes = [
  { path: '', component: ConsignaComponent },
  { path: 'reflection', component: ReflectionComponent, canActivate: [reflectionLimitGuard] },
];

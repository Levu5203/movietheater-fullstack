import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './shared/layouts/admin-layout/admin-layout.component';
import { CustomerLayoutComponent } from './shared/layouts/customer-layout/customer-layout.component';

export const routes: Routes = [
    {
        path: 'admin',
        component: AdminLayoutComponent,
        loadChildren: () =>
          import('./admin/admin.module').then((m) => m.AdminModule),
      },
    {
        path: '',
        component: CustomerLayoutComponent,
        loadChildren: () =>
          import('./customer/customer.module').then((m) => m.CustomerModule),
      }
];

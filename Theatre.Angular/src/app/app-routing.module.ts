import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { AuthGuard } from './auth/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { SpectacleComponent } from './spectacle/spectacle.component';
import { SpectacleDetailsComponent } from './spectacle/details/spectacle.details.component';

const routes: Routes = [
  {
    path: '', redirectTo: '/user/login', pathMatch: 'full'
  },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  {
    path: 'home', component: HomeComponent, canActivate: [AuthGuard]
  },
  {
    path: 'spectacles', component: SpectacleComponent
  },
  {
    path: 'spectacles/:id', component: SpectacleDetailsComponent
  },
  {
    path: 'forbidden', component: ForbiddenComponent
  },
  {
    path: 'adminpanel', component: AdminPanelComponent, canActivate: [AuthGuard], data: {
      permittedRoles: ['Administrator']
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

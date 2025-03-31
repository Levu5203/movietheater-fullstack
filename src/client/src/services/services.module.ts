import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { USER_SERVICE } from '../constants/injection.constant';
import { UserService } from './user/user.service';
import { ProfileService } from './profile.service';

@NgModule({
  declarations: [],
  imports: [CommonModule],
  providers: [
    {
      provide: USER_SERVICE,
      useClass: UserService,
    },
    {
      provide: 'profileService',
      useClass: ProfileService,
    },
  ],
})
export class ServicesModule {}

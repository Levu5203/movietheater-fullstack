import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PROMOTION_SERVICE, USER_SERVICE } from '../constants/injection.constant';
import { UserService } from './user/user.service';
import { ProfileService } from './profile.service';
import { PromotionService } from './promotion/promotion-service';

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
    {
      provide: PROMOTION_SERVICE,
      useClass: PromotionService,
    }
  ],
})
export class ServicesModule {}

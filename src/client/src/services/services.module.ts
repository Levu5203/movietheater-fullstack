import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ROOM_SERVICE, USER_SERVICE } from '../constants/injection.constant';
import { UserService } from './user/user.service';
import { ProfileService } from './profile.service';
import { RoomService } from './room/room.service';

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
      provide: ROOM_SERVICE,
      useClass: RoomService,
    }
  ],
})
export class ServicesModule {}

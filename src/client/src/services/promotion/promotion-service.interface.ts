import { IMasterDataService } from '../master-data/master-data.service.interface';
import { UserModel } from '../../models/user/user.model';
import { PromotionModel } from '../../models/promotion/promotion.model';

export interface IPromotionService extends IMasterDataService<PromotionModel> {}

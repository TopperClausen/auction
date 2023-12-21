import { get } from './requestService';

export const all = () => {
    return get(`brands`);
}

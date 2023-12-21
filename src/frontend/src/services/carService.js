import { get, post } from './requestService';

export const all = (userID, jwt) => {
    return get(`users/${userID}/cars`, jwt);
}

export const create = (jwt, brandID, userID, name, kilometersDriven, kilometersPerLiter, modelYear, latestPlate) => {
    const body = {
        brandID,
        userID,
        name,
        kilometersDriven,
        kilometersPerLiter,
        modelYear,
        latestPlate
    }

    return post(`users/${userID}/cars`, body, jwt);
}

import { defineStore } from 'pinia';

export const useSessionStore = defineStore('session', {
    state: () => {
        return {
            jwt: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBhZG1pbi5kayIsImV4cCI6MTcwNTcxNjU3OCwiaXNzIjoiU2VjcmV0TWUiLCJhdWQiOiJTZWNyZXRZb3UifQ.qBWVFIILNW2F86GwRodMLcUyZfAEyOTv4GcWNE67KEA',
            userID: 1
        }
    },
    actions: {
        setJwt(jwt) {
            this.jwt = jwt;
        },
        setUserID(id) {
            this.userID = id;
        },
        clearSession() {
            this.jwt = '';
            this.userID = -1;
        }
    }
}, { persist: true })
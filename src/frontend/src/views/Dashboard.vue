<script setup>
import CarsList from '../components/CarsList.vue';
import SubmitButton from '../components/SubmitButton.vue';
import { useSessionStore } from '../stores/session'
import { useRouter } from 'vue-router'

const session = useSessionStore();
const router = useRouter();

if(!session.jwt) {
    router.push({ name: 'Login' });
}

</script>

<template>
    <div class="grid grid-cols-4 mt-5 mx-5">
        <div class="col-span-3">
            <span> bruh</span>
        </div>
        <div>
            <div class="flex justify-between">
                <h3> Dine biler </h3>
                <SubmitButton @click="router.push({ name: 'NewCar' })"> Opret bil </SubmitButton>
            </div>
            <CarsList/>
        </div>
    </div>
</template>
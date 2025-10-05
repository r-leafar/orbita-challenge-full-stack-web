<template>
    <v-dialog v-model="visible" max-width="400">
        <v-card>
            <v-card-title class="text-h6">{{ title }}</v-card-title>
            <v-card-text>{{ message }}</v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="cancel">Cancelar</v-btn>
                <v-btn color="red" @click="confirm">Confirmar</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script setup lang="ts">
import { computed, defineProps, defineEmits } from 'vue';

const props = defineProps({
    title: { type: String, default: 'Confirmar' },
    message: { type: String, required: true },
    modelValue: { type: Boolean, default: false }
});

const emit = defineEmits(['update:modelValue', 'confirm']);

const visible = computed({
    get: () => props.modelValue,
    set: (val: boolean) => emit('update:modelValue', val)
});

function confirm() {
    emit('confirm');
    visible.value = false;
}

function cancel() {
    visible.value = false;
}

</script>

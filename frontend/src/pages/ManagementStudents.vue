<template>
    <AppMenuBar :show-menu="true">
        <slot>
            <v-main>
                <v-container>
                    <h1>Gerenciar Alunos</h1>
                    <v-row>
                        <v-col cols="12" md="8">

                            <v-text-field :append-icon="'mdi-account-plus-outline'"
                                :append-inner-icon="'mdi-account-search-outline'" @keydown.enter="searchStudent"
                                @click:append-inner="searchStudent" @click:append="openRegisterStudentForm()"
                                v-model="searchTerm" label="Nome do aluno">
                            </v-text-field>
                            <v-dialog v-model="registerStudentDialog" :width="$vuetify.display.xs ? '320px' : '500px'">
                                <StudentForm :mode="currentMode"
                                    @closeRegisterStudent="registerStudentDialog = false" />
                            </v-dialog>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col v-for="item in studentStore.students.sort((a, b) => a.Name.localeCompare(b.Name))"
                            cols="12" sm="6" md="4" lg="3">
                            <v-card flat class="border" :key="item.Id">
                                <v-card-subtitle class="pt-3">Informações</v-card-subtitle>
                                <v-card-text>
                                    <v-list density="compact">
                                        <v-list-item>
                                            <v-list-item-title class="font-weight-medium">Nome:</v-list-item-title>
                                            <v-list-item-subtitle>{{ item.Name }}</v-list-item-subtitle>
                                        </v-list-item>

                                        <v-list-item>
                                            <v-list-item-title class="font-weight-medium">RA:</v-list-item-title>
                                            <v-list-item-subtitle>{{ item.StudentId }}</v-list-item-subtitle>
                                        </v-list-item>

                                        <v-list-item>
                                            <v-list-item-title class="font-weight-medium">CPF:</v-list-item-title>
                                            <v-list-item-subtitle>{{ item.NationalIdValue }}</v-list-item-subtitle>
                                        </v-list-item>
                                    </v-list>

                                    <ConfirmDialog v-model="confirmDialog" title="Confirmar Ação"
                                        message="Tem certeza que deseja excluir este aluno?"
                                        @confirm="deleteStudent()" />
                                </v-card-text>

                                <v-card-actions>
                                    <v-btn @click="openEditStudentForm(item)" variant="tonal"
                                        color="gray">Editar</v-btn>

                                    <v-btn @click="openConfirmDialog(item.Id)" variant="tonal" color="red">Excluir
                                    </v-btn>
                                </v-card-actions>
                            </v-card>
                        </v-col>
                    </v-row>
                </v-container>
            </v-main>
        </slot>
    </AppMenuBar>
</template>

<script setup lang="ts">
import { onMounted, ref, reactive, computed, watch } from 'vue';
import { useStudentStore } from '@/stores/studentStore'
import { useNotificationStore } from '@/stores/notificationStore';
import type { Student } from '@/types/student';

const searchTerm = ref('');
const registerStudentDialog = ref(false);
const confirmDialog = ref(false);
const idForDelete = ref('');
const currentMode = ref<'create' | 'edit'>('create');

const studentStore = useStudentStore();
const notificationStore = useNotificationStore();

const searchTermIsEmpty = computed(() => searchTerm.value.trim() === '');

watch(searchTermIsEmpty, (newValue) => {
    if (newValue === true) {
        studentStore.getStudentsPaged(1);
    }
});


function openConfirmDialog(id: string) {
    confirmDialog.value = true;
    idForDelete.value = id;
    console.log(idForDelete.value);
}
function deleteStudent() {
    studentStore.deleteStudent(idForDelete.value);
    notificationStore.showSnackbar('Aluno excluído com sucesso!', 'error');
}

function searchStudent() {
    if (searchTerm.value.trim() === '') {
        studentStore.getStudentsPaged(1);
        return;
    }
    studentStore.getStudentsByNamePaged(searchTerm.value, 1);
}
function openRegisterStudentForm() {
    currentMode.value = "create";
    studentStore.cleanSelection();
    registerStudentDialog.value = true;
}
function openEditStudentForm(student: Student) {
    currentMode.value = "edit";
    studentStore.setStudentToEdit(student);
    registerStudentDialog.value = true;
}

onMounted(() => {
    studentStore.getStudentsPaged();
});
</script>
# Clicker
Mobile clicker game


СОЗДАНИЕ ВРАГА:
   - Для возможности дамажить врага добавляется компонент Damageable, там же задается его максимальное хп.
   - У врага обязательно должен быть Animator. Если враг имеет компонент Damageable, то должна присутствовать анимация  с названием TakeDamage и переменная bool IsDead.
   - Если нужно, чтобы при определенных условиях враг выбрасывал предметы (например, при смерти), добавить на него Dropper

НАСТРОЙКА DROPPER'A:
   - Дроппер может хранить множество предметов.
   - Для каждого предмета модель, тип, количество и шанс выпадения настраиваются отдельно.
   - Можно настроить разное количество выпадаемых преметов одного и того же типа, для каждого количества прописывается свой шанс выпадения. Во время выбрасывания Дроппер просчитывает все возможные варианты и выбирает максимальный получившийся.
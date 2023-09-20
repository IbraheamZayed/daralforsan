let DateFormat = {
    getTime: function getTime(date, lang, enableSeconds = false) {
        let hours = date.getHours();
        let minutes = date.getMinutes();
        let second = date.getSeconds();
        let ampm = hours >= 12 ? 'م' : 'ص';
        if (lang == "en")
            ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        second = second < 10 ? '0' + second : second;
        if (enableSeconds = false) {
            return `${hours}:${minutes} ${ampm}`;
        }
        else
            return `${hours}:${minutes}:${second} ${ampm}`;
    },

    getTimeAndDayName: function getTimeAndDayName(date, lang) {
        const weekday_en = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        const weekday_ar = ["الأحد", "الإثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"];
        let time = this.getTime(date, lang);
        let day = weekday_ar[date.getDay()];
        let result_date = `${day} في ${time}`;
        if (lang == "en") {
            day = weekday_en[date.getDay()];
            result_date = `${day} at ${time}`;
        }
        return result_date;
    },

    getFullDate: function getFullDate(date, lang) {
        const monthNames_en = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        const monthNames_ar = ["يناير", "فبراير", "مارس", "ابريل", "مايو", "يونيه", "يوليه", "اغسطس", "سبتمبر", "اكتوبر", "نوفمبر", "ديسمبر"];
        let time = this.getTime(date, lang);
        if (lang == "en")
            return `${date.getDate()} ${monthNames_en[date.getMonth()]} ${date.getFullYear()} at ${time}`;
        return `${date.getDate()} ${monthNames_ar[date.getMonth()]} ${date.getFullYear()} في ${time}`;
    },

    isTheSameDate: function (date1, date2) {
        let temp_date1 = new Date(date1), temp_date2 = new Date(date2);
        return (temp_date1.setHours(0, 0, 0, 0) - temp_date2.setHours(0, 0, 0, 0)) == 0;
    },

    getDiffDays: function getDiffDays(date1, date2) {
        return (date1.getTime() - date2.getTime()) / (1000 * 3600 * 24);
    },
    getDiffMinutes: function getDiffMinutes(date1, date2) {
        return Math.round(Math.floor((date1.getTime() - new Date(date2).getTime()) / 1000) / 60);
    },
    displayDate: function displayDate(utcDate, lang) {
        let local_date = new Date(utcDate);
        //let hoursDiff = local_date.getHours() - local_date.getTimezoneOffset() / 60;
        //let minutesDiff = (local_date.getHours() - local_date.getTimezoneOffset()) % 60;
        //local_date.setHours(hoursDiff);
        //local_date.setMinutes(minutesDiff);
        let current_date = new Date();
        if (this.isTheSameDate(local_date, current_date)) {
            return this.getTime(local_date, lang);
        }
        else if (this.getDiffDays(current_date, local_date) <= 6) {
            return this.getTimeAndDayName(local_date, lang);
        }
        else {
            return this.getFullDate(local_date, lang);
        }
    }
}
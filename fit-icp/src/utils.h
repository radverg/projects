/**
 * @file utils.h
 * @author xvever13 (Radek Veverka)
 * @author xsysmi00 (Michal Sýs)
 * @date May 2020
 *
 * ICP Project, VUT FIT
 *
 * Contains declaration of utility functions.
 */

#ifndef UTILS_H
#define UTILS_H

#include <iostream>
#include <QPoint>
#include <QTime>

namespace Utils {
    namespace Math {
        /**
         * @brief Computes coordinations of a point between two points.
         * @param linestart The start point.
         * @param lineend The end point.
         * @param factor From range <0,1>, indicates how far is the desired point distanced from the start point.
         * @return Coordinates of the point moved from the start point to the end point by factor.
         */
        QPoint position_on_line(QPoint linestart, QPoint lineend, float factor);

        /**
         * @brief Computes distance between two points using Pythagorean theorem.
         * @param first First point.
         * @param second Second point.
         * @return Distance between points first and second.
         */
        float qpoint_distance(const QPoint& first, const QPoint& second);

        /**
         * @brief Translates real-world coordinates (latitude and longtitude) to pixel coordinates.
         *
         * Please note that since this translation is not simple,
         * this function does work only within a small area of Miami (required for the extension of this project),
         * where points can be scaled linearly.
         *
         * @param latitude y-coordinate (angle)
         * @param longitude x-coordinate (angle)
         * @return Translated point to pixels.
         */
        QPoint real_to_coord(float latitude, float longitude);
    }

    namespace Time {
        /**
         * @brief Computes amount of seconds between two QTime objects, taking midnight into consideration.
         *
         * Order of time arguments matters and is used for dealing with midnight pass.
         *
         * @param starttime Start time (QTime).
         * @param endtime End time (QTime).
         * @return Number of seconds between given times.
         */
        int seconds_between_times(const QTime& starttime, const QTime& endtime);

        /**
         * @brief Checks whether one QTime is between two other QTimes, taking midnight into consideration.
         * @param starttime From which time the range starts.
         * @param endtime At which time the range ends.
         * @param midtime Time to be checked against the range.
         * @return Whether midtime is between starttime and endtime.
         */
        bool is_time_between_times(const QTime& starttime, const QTime& endtime, const QTime& midtime);
    }
}

#endif // UTILS_H
